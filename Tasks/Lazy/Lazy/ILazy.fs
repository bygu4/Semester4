namespace Lazy

/// An interface representing a lazy evaluation.
type ILazy<'a> =
    /// Get the result evaluated lazily.
    abstract member Get: unit -> 'a
