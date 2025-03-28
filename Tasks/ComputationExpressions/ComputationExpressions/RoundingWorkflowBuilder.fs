namespace ComputationExpressions

open System

type RoundingWorkflowBuilder (precision: int) =
    do ArgumentOutOfRangeException.ThrowIfNegative precision

    member _.Bind (value: float, cont: float -> float) =
        Math.Round (value, precision) |> cont

    member _.Return value: float = value
