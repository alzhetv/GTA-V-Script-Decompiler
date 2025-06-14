﻿using System;

namespace Decompiler.Ast
{
    internal class LoadN : AstToken
    {
        public readonly AstToken Pointer;
        public readonly AstToken Count;

        public LoadN(Function func, AstToken pointer, AstToken count) : base(func)
        {
            Pointer = pointer;
            Count = count;
        }

        public override int GetStackCount()
        {
            return Count is not ConstantInt
                ? throw new InvalidOperationException("Cannot retrieve load size as it is not a constant")
                : (int)(Count as ConstantInt).GetValue();
        }

        public override string ToString()
        {
            if (Pointer.IsPointer())
                return Pointer.ToPointerString(); // TODO: maybe change this?
            else
                return "*" + Pointer.ToString();
        }

#if false
        public override void HintType(Stack.DataType type)
        {
            if (Types.HasPointerVersion(type))
                Pointer.HintType(Types.GetPointerVersion(type));
        }
#endif
    }
}
