﻿#include <Bibim/Config.h>
#include <Bibim/UIDomain.h>
#include <Bibim/UIWindow.h>

namespace Bibim
{
    UIDomain::UIDomain()
        : root(nullptr)
    {
    }
    
    UIDomain::UIDomain(UIWindow* root)
        : root(root)
    {
    }

    UIDomain::~UIDomain()
    {
    }

    void UIDomain::SetRoot(UIWindow* value)
    {
        root = value;
    }

    void UIDomain::Focus(UIVisual* target)
    {
        target->Focus(this);
    }

    void UIDomain::Blur(UIVisual* target)
    {
        target->Focus(nullptr);
    }
}