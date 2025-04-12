module Parentheses.Tests

open NUnit.Framework
open FsUnit

let testCases =
    [
        "", true;
        "(", false;
        "]", false;
        "([])", true;
        "([)]", false;
        "{()[]}", true;
        "ololo(qwerty)", true;
        "({[{}]}(()))", true;
        "]a]", false;
        "{ew", false;
        "([]))", false;
        "{          }", true;
        "{[)}", false;
        "       ({}[]) ", true;
    ] |> List.map TestCaseData

[<TestCaseSource(nameof(testCases))>]
let testCheckParentheses (string, isCorrectSequence) =
    hasCorrectParentheses string |> should equal isCorrectSequence
