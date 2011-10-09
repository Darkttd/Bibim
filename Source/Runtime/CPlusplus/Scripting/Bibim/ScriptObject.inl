namespace Bibim
{
    ScriptObject::ScriptObject()
        : type(VoidType)
    {
    }

    ScriptObject::ScriptObject(bool value)
        : type(BooleanType),
          value(value)
    {
    }

    ScriptObject::ScriptObject(int value)
        : type(IntType),
          value(value)
    {
    }

    ScriptObject::ScriptObject(uint value)
        : type(UIntType),
          value(value)
    {
    }

    ScriptObject::ScriptObject(int64 value)
        : type(FloatType),
          value(value)
    {
    }

    ScriptObject::ScriptObject(float value)
        : type(FloatType),
          value(value)
    {
    }

    ScriptObject::ScriptObject(Color value)
        : type(ColorType),
          value(static_cast<uint>(value.ToARGB()))

    {
    }

    ScriptObject::ScriptObject(Vector2 value)
        : type(Float2Type),
          value(value.X, value.Y)
    {
    }

    ScriptObject::ScriptObject(Vector3 value)
        : type(Float3Type),
          value(new SixteenBytes(value.X, value.Y, value.Z, 0.0f))
    {
    }

    ScriptObject::ScriptObject(Vector4 value)
        : type(Float3Type),
          value(new SixteenBytes(value.X, value.Y, value.Z, value.W))
    {
    }

    ScriptObject::ScriptObject(const char* value)
        : type(StringType),
          value(new String(value))
    {
    }

    ScriptObject::ScriptObject(const String& value)
        : type(StringType),
          value(new String(value))
    {
    }

    ScriptObject::ScriptObject(SharedObject* value)
        : type(SharedObjectType),
          value(new SharedObjectPtr(value))
    {
    }

    ScriptObject::ScriptObject(void* value)
        : type(LightObjectType),
          value(value)
    {
    }

    ScriptObject::Type ScriptObject::GetType() const
    {
        return type;
    }

    bool ScriptObject::operator != (const ScriptObject& right) const
    {
        return !operator == (right);
    }

    int ScriptObject::SizeOf(Type value)
    {
        switch (value)
        {
            case VoidType:
                return 0;
            case BooleanType:
                return sizeof(bool);
            case IntType:
                return sizeof(int);
            case UIntType:
                return sizeof(uint);
            case LongIntType:
                return sizeof(int64);
            case FloatType:
                return sizeof(float);
            case ColorType:
                return sizeof(uint);
            case Int2Type:
                return sizeof(int) * 2;
            case Int3Type:
                return sizeof(int) * 3;
            case Int4Type:
                return sizeof(int) * 4;
            case Float2Type:
                return sizeof(float) * 2;
            case Float3Type:
                return sizeof(float) * 3;
            case Float4Type:
                return sizeof(float) * 4;
            case StringType:
                throw;
            case SharedObjectType:
                throw;
            case LightObjectType:
                throw;
            default:
                return 0;
        }
    }
}