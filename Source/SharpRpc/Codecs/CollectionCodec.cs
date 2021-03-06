﻿#region License
/*
Copyright (c) 2013-2014 Daniil Rodin of Buhgalteria.Kontur team of SKB Kontur

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using SharpRpc.Utility;

namespace SharpRpc.Codecs
{
    public class CollectionCodec : CollectionCodecBase
    {
        private readonly ConstructorInfo collectionConstructor;

        private readonly MethodInfo getCountMethod;
        private readonly MethodInfo addMethod;

        public CollectionCodec(Type type, Type elementType, ICodecContainer codecContainer)
            : base(type, elementType, codecContainer)
        {
            collectionConstructor = type.GetConstructor(Type.EmptyTypes);
            getCountMethod = typeof(ICollection<>).MakeGenericType(elementType).GetMethod("get_Count");
            addMethod = typeof(ICollection<>).MakeGenericType(elementType).GetMethod("Add");
        }

        protected override void EmitCreateCollection(MyILGenerator il, LocalBuilder lengthVar)
        {
            il.Newobj(collectionConstructor);
        }

        protected override void EmitLoadCount(MyILGenerator il, Action<MyILGenerator> emitLoad)
        {
            emitLoad(il);
            il.Callvirt(getCountMethod);
        }

        protected override void EmitDecodeAndStore(IEmittingContext context, LocalBuilder collectionVar, Action emitLoadIndex, bool doNotCheckBounds)
        {
            var il = context.IL;
            il.Ldloc(collectionVar);
            ElementCodec.EmitDecode(context, doNotCheckBounds);
            il.Callvirt(addMethod);
        }
    }
}
