﻿#include <Bibim/Config.h>
#include <Bibim/Command.h>

namespace Bibim
{
    Command::Command()
    {
    }

    Command::~Command()
    {
    }

    bool Command::IsRestorable() const
    {
        return false;
    }
}