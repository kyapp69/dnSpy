/*
    Copyright (C) 2014-2019 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;

namespace dnSpy.Debugger.DotNet.Metadata.Impl.MD {
	sealed class DmdParameterDefMD : DmdParameterDef {
		public sealed override string Name { get; }
		public sealed override DmdParameterAttributes Attributes { get; }

		readonly DmdEcma335MetadataReader reader;

		public DmdParameterDefMD(DmdEcma335MetadataReader reader, uint rid, string name, DmdParameterAttributes attributes, DmdMemberInfo member, int position, DmdType parameterType) : base(rid, member, position, parameterType) {
			this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
			Name = name;// Null is allowed
			Attributes = attributes;
		}

		protected override (DmdCustomAttributeData[] cas, DmdMarshalType marshalType) CreateCustomAttributes() {
			var cas = reader.ReadCustomAttributes(MetadataToken);
			var marshalType = reader.ReadMarshalType(MetadataToken, Member.Module, null);
			return (cas, marshalType);
		}

		protected override (object rawDefaultValue, bool hasDefaultValue) CreateDefaultValue() => reader.ReadConstant(MetadataToken);
	}
}
