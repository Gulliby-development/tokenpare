<p align="center"><img src="docs/logo.svg" width="96" alt="" /></p>

# tokenpare

[![CI](https://github.com/Gulliby-development/tokenpare/actions/workflows/ci.yml/badge.svg)](https://github.com/Gulliby-development/tokenpare/actions/workflows/ci.yml) [![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

tokenpare replays the cuts that a language model sampler makes before it picks the next token: top-k, top-p and min-p. You give it a short list of weighted candidates on standard input and it shows what each filter keeps. It makes no model call, uses no random numbers and no clock, and does all its arithmetic on integers, so the same input always gives the same output.

It is for people who tune or debug a sampler and want to replay a cut on a list they can read, in a terminal, in a script or in CI. It is written in C# on .NET 10 and uses the standard library only.

Run it from the repository directory:

```text
dotnet run --project src/Tokenpare -- COMMAND
```

In the examples below, `tokenpare` stands for a shell function that runs `dotnet run --project src/Tokenpare --no-build -- "$@"`, defined in a POSIX shell in the repository directory after a `dotnet build`.

## Usage

```text
usage: tokenpare check
       tokenpare share [--unit UNIT] [--format FORMAT]
       tokenpare topk --k N [--format FORMAT]
       tokenpare topp --p PERMILLE [--format FORMAT]
       tokenpare minp --ratio PERMILLE [--format FORMAT]
       tokenpare chain --steps STEPS [--format FORMAT]
       tokenpare draw --at N
```

- `check` validates the list and prints the number of candidates and the total weight.
- `share` shows each weight as a share of the total, in input order.
- `topk` keeps the N heaviest candidates.
- `topp` keeps the smallest group of heaviest candidates whose weights reach a share of the total.
- `minp` keeps the candidates that weigh at least a ratio of the heaviest one.
- `chain` applies several filters one after the other.
- `draw` names the candidate that a position in the cumulative weights falls on.

`help`, `-h` and `--help` as the first word print the usage and exit 0. `--version` prints `tokenpare 0.1.0` and exits 0. With no word the usage goes to standard error and the exit status is 2.

Options take the next word as their value; there is no `--option=value` form. The default format is `plain` and the default unit is `permille`. Option values:

- `--format`: `plain`, `csv` or `bar`.
- `--unit`: `permille`, `percent` or `ppm`.
- `--k`: 1 to 32. `--p`: 1 to 1000. `--ratio`: 0 to 1000. `--at`: 0 to 999999999. Write them without a sign or a leading zero.
- `--steps`: one to four steps separated by commas, each `topk:N`, `topp:P` or `minp:R` with a value in the range of the option of the same name, for example `topk:3,minp:100`.

## List format

Every command reads the list from standard input, one candidate per line: a token and an integer weight, separated by spaces or tabs.

- A token has 1 to 12 characters from `A-Z`, `a-z`, `0-9`, `_`, `.` and `-`. Upper and lower case are different tokens, and a token may appear only once.
- A weight is `0` or up to six decimal digits with no leading zero and no sign (`0` to `999999`).
- Blank lines and lines whose first word starts with `#` are skipped.
- At most 32 lines are allowed, skipped lines included.
- A CR at the end of a line is dropped; a last line without a line feed still counts.
- The list needs at least one candidate and a total weight above zero.

## Formats

A table has one line per candidate, with a token and a number:

- `plain`: `TOKEN NUMBER`
- `csv`: `TOKEN,NUMBER`
- `bar`: `TOKEN NUMBER MARKS`, where MARKS is a run of `#` characters. A number of 0 has no marks, so its line ends with a space.

The marks are the number times 20 divided by the full bar, rounded down, at least one for a number above 0 and at most 20. The full bar is the unit scale for `share` (1000, 100 or 1000000) and the heaviest weight of the table for the other commands.

`share` prints the share of each weight in the total as a whole number in the chosen unit: `permille` (scale 1000), `percent` (scale 100) or `ppm` (scale 1000000). A half rounds up, so the shares need not add up to the scale.

## Filters

The ranking puts the heaviest candidate first; candidates with the same weight keep their input order. The filters print what they keep in ranking order, with the weights unchanged.

- `topk --k N` keeps the N heaviest candidates, or all of them when the list is shorter.
- `topp --p PERMILLE` walks down the ranking and keeps candidates up to and including the first one at which the weight kept so far, times 1000, reaches P times the total weight.
- `minp --ratio PERMILLE` keeps the candidates whose weight times 1000 reaches RATIO times the heaviest weight. The heaviest candidate always stays.
- `chain --steps STEPS` applies its steps in order, each on the survivors of the step before it. A `topp` step uses the total weight of the candidates that are left, and a `minp` step uses the heaviest weight of the candidates that are left.
- `draw --at N` works on the cumulative weights in input order, not in ranking order. It prints the token of the first candidate whose running total after its own weight is above N.

## Examples

The commands below assume the shell function described above, in a POSIX shell in the repository directory. `2>&1` shows the error line.

```text
$ printf 'the 500\ncat 300\ndog 150\nsat 40\nmat 10\n' | tokenpare topp --p 900
the 500
cat 300
dog 150
$ printf 'the 500\ncat 300\ndog 150\n' | tokenpare share --unit percent --format bar; printf 'a 1\nb 3\n' | tokenpare share --format csv
the 53 ##########
cat 32 ######
dog 16 ###
a,250
b,750
$ printf 'the 500\ncat 300\ndog 150\nsat 40\nmat 10\n' | tokenpare chain --steps topk:4,minp:100; printf 'the 500\ncat 300\ndog 150\n' | tokenpare draw --at 520
the 500
cat 300
dog 150
cat
$ printf 'a 1\nb\n' | tokenpare check 2>&1; printf 'a 1\n' | tokenpare topk 2>&1
tokenpare: line 2: expected TOKEN WEIGHT
tokenpare: missing --k
```

## Errors

A failing command writes nothing to standard output and one line `tokenpare: MESSAGE` to standard error.

Exit statuses:

- 0: success, including help and version.
- 1: an error in the list or in a command.
- 2: an error in the command line, including a missing command word.

Command line errors (status 2, nothing is read), checked word by word from left to right:

- `tokenpare: unknown command WORD`
- `tokenpare: unexpected argument WORD`
- `tokenpare: unknown option WORD`
- `tokenpare: option OPTION is not valid for COMMAND`
- `tokenpare: given twice: OPTION`
- `tokenpare: missing value for OPTION`
- `tokenpare: bad value for OPTION`
- `tokenpare: missing --k`, `missing --p`, `missing --ratio`, `missing --steps` or `missing --at`, each with the `tokenpare: ` prefix

List and command errors (status 1), where N is the line number counted from 1:

- `tokenpare: input is too long (max 32 lines)`
- `tokenpare: line N: expected TOKEN WEIGHT`
- `tokenpare: line N: bad token WORD`
- `tokenpare: line N: bad weight WORD`
- `tokenpare: line N: duplicate token WORD`
- `tokenpare: no candidates`
- `tokenpare: total weight is zero`
- `tokenpare: draw position N is outside the total T`

## Development

From the repository directory, with the .NET 10 SDK on Windows, macOS or Linux:

```text
dotnet build
dotnet format --verify-no-changes
dotnet test
```

---

Built with [BAKED](https://github.com/Gulliby-development/baked), an open-source coding agent (bench round R106).
