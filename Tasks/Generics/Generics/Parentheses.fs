module Parentheses

let openingParentheses = dict (seq { '(', ')'; '[', ']'; '{', '}' })
let closingParentheses = set (seq { ')'; ']'; '}' })

let hasCorrectParentheses (string: string) =
    let chars = Seq.toList string
    let rec checkParenthesesInternal openPars chars =
        match chars with
        | [] -> List.isEmpty openPars
        | c :: rest when openingParentheses.ContainsKey c ->
            checkParenthesesInternal (c :: openPars) rest
        | c :: rest when closingParentheses.Contains c ->
            if not openPars.IsEmpty && openingParentheses[List.head openPars] = c then
                checkParenthesesInternal (List.tail openPars) rest
            else false
        | _ :: rest -> checkParenthesesInternal openPars rest
    checkParenthesesInternal [] chars
