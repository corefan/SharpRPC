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

using SharpRpc.Codecs;

namespace SharpRpc.ClientSide
{
    public class InliningServiceProxyMethodIoCodec : IServiceProxyMethodIoCodec
    {
        private readonly IEmittingCodec emittingCodec;
        private readonly IServiceProxyMethodParameterAccessor parameterAccessor;

        public InliningServiceProxyMethodIoCodec(IEmittingCodec emittingCodec, IServiceProxyMethodParameterAccessor parameterAccessor)
        {
            this.emittingCodec = emittingCodec;
            this.parameterAccessor = parameterAccessor;
        }

        public void EmitCalculateSize(IServiceProxyClassBuildingContext classContext, IEmittingContext emittingContext)
        {
            emittingCodec.EmitCalculateSize(emittingContext, parameterAccessor.EmitLoad);
        }

        public void EmitEncode(IServiceProxyClassBuildingContext classContext, IEmittingContext emittingContext)
        {
            emittingCodec.EmitEncode(emittingContext, parameterAccessor.EmitLoad);
        }

        public void EmitDecode(IServiceProxyClassBuildingContext classContext, IEmittingContext emittingContext)
        {
            emittingCodec.EmitDecode(emittingContext, false);
        }

        public void EmitDecodeAndStore(IServiceProxyClassBuildingContext classContext, IEmittingContext emittingContext)
        {
            parameterAccessor.EmitBeginStore(emittingContext.IL);
            EmitDecode(classContext, emittingContext);
            parameterAccessor.EmitEndStore(emittingContext.IL);
        }
    }
}