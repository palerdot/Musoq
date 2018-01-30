﻿namespace FQL.Evaluator.Instructions.Arythmetic
{
    public class EqualLongInstruction : ByteCodeInstruction
    {
        public override void Execute(IVirtualMachine virtualMachine)
        {
            var stack = virtualMachine.Current.LongsStack;
            virtualMachine.Current.BooleanStack.Push(stack.Pop() == stack.Pop());

            virtualMachine[Register.Ip] += 1;
        }

        public override string DebugInfo()
        {
            return "EQUALS LNGS";
        }
    }
}