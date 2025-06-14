﻿namespace Decompiler.Ast
{
    internal class Offset : AstToken
    {
        public readonly AstToken value;
        public readonly AstToken offset;

        public Offset(Function func, AstToken value, AstToken offset) : base(func)
        {
            this.value = value;
            this.offset = offset;
        }

        public override string ToString()
        {
            var sep = "->";
            if (value.IsPointer())
                sep = ".";

            return offset is ConstantInt
                ? "&(" + value.ToPointerString() + sep + "f_" + offset.ToString() + ")"
                : "&(" + value.ToPointerString() + sep + "f_[" + offset.ToString() + "])";
        }

        public override string ToPointerString()
        {
            var sep = "->";
            if (value.IsPointer())
                sep = ".";

            return offset is ConstantInt
                ? value.ToPointerString() + sep + "f_" + (offset as ConstantInt).GetValue()
                : value.ToPointerString() + sep + "f_[" + offset.ToString() + "]";
        }

        public override bool CanGetGlobalIndex() => value.CanGetGlobalIndex() && offset is ConstantInt;

        public override int GetGlobalIndex() => value.GetGlobalIndex() + (int)(offset as ConstantInt).GetValue();

        public override bool IsPointer() => true;
    }

    internal class OffsetLoad : AstToken
    {
        public readonly AstToken value;
        public readonly int offset;

        public OffsetLoad(Function func, AstToken value, int offset) : base(func)
        {
            this.value = value;
            this.offset = offset;
        }

        public override string ToString()
        {
            var sep = "->";
            if (value.IsPointer())
                sep = ".";

            return value.ToPointerString() + sep + "f_" + offset.ToString();
        }

        public override bool CanGetGlobalIndex() => value.CanGetGlobalIndex();

        public override int GetGlobalIndex() => value.GetGlobalIndex() + offset;
    }

    internal class OffsetStore : AstToken
    {
        public readonly AstToken value;
        public readonly AstToken storedValue;
        public readonly int offset;

        public OffsetStore(Function func, AstToken value, int offset, AstToken storedValue) : base(func)
        {
            this.value = value;
            this.offset = offset;
            this.storedValue = storedValue;

            HintType(ref storedValue.GetTypeContainer());
        }

        public override bool IsStatement() => true;

        public override string ToString()
        {
            var sep = "->";
            if (value.IsPointer())
                sep = ".";

            return value.ToPointerString() + sep + "f_" + offset.ToString() + " = " + storedValue.ToString() + ";";
        }
    }
}
