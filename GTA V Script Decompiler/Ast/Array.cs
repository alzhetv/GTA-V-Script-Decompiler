﻿namespace Decompiler.Ast
{
    internal class Array : AstToken
    {
        public readonly uint Size;
        public readonly AstToken Pointer;
        public readonly AstToken Index;

        public Array(Function func, uint size, AstToken pointer, AstToken index) : base(func)
        {
            Size = size;
            Pointer = pointer;
            Index = index;

            Index.HintType(ref Types.INT.GetContainer());
        }

        public override string ToString()
        {
            var sep = "->";
            if (Pointer.IsPointer())
                sep = "";

            return "&" + Pointer.ToPointerString() + sep + "[" + Index.ToString() + Stack.GetArraySizeCmt(Size) + "]";
        }

        public override string ToPointerString()
        {
            var sep = "->";
            if (Pointer.IsPointer())
                sep = "";

            return Pointer.ToPointerString() + sep + "[" + Index.ToString() + Stack.GetArraySizeCmt(Size) + "]";
        }

        public override bool CanGetGlobalIndex() => Pointer.CanGetGlobalIndex();

        public override int GetGlobalIndex() => Pointer.GetGlobalIndex() + 1;

        public override bool IsPointer() => true;
    }

    internal class ArrayLoad : AstToken
    {
        public readonly uint Size;
        public readonly AstToken Pointer;
        public readonly AstToken Index;

        public ArrayLoad(Function func, uint size, AstToken pointer, AstToken index) : base(func)
        {
            Size = size;
            Pointer = pointer;
            Index = index;

            Index.HintType(ref Types.INT.GetContainer());
        }

        public override string ToString()
        {
            var sep = "->";
            if (Pointer.IsPointer())
                sep = "";

            return Pointer.ToPointerString() + sep + "[" + Index.ToString() + Stack.GetArraySizeCmt(Size) + "]";
        }

        public override bool CanGetGlobalIndex() => Pointer.CanGetGlobalIndex();

        public override int GetGlobalIndex() => Pointer.GetGlobalIndex() + 1;
    }

    internal class ArrayStore : AstToken
    {
        public readonly uint Size;
        public readonly AstToken Pointer;
        public readonly AstToken Index;
        public readonly AstToken Value;

        public ArrayStore(Function func, uint size, AstToken pointer, AstToken index, AstToken value) : base(func)
        {
            Size = size;
            Pointer = pointer;
            Index = index;
            Value = value;

            Index.HintType(ref Types.INT.GetContainer());

            HintType(ref value.GetTypeContainer());
        }

        public override bool IsStatement() => true;

        public override string ToString()
        {
            var sep = "->";
            if (Pointer.IsPointer())
                sep = "";

            return Pointer.ToPointerString() + sep + "[" + Index.ToString() + Stack.GetArraySizeCmt(Size) + "] = " + Value.ToString() + ";";
        }

        public override bool CanGetGlobalIndex() => Pointer.CanGetGlobalIndex();

        public override int GetGlobalIndex() => Pointer.GetGlobalIndex() + 1;
    }
}
