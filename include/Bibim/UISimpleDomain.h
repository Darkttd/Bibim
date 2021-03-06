﻿#pragma once
#ifndef __BIBIM_UISIMPLEDOMAIN_H__
#define __BIBIM_UISIMPLEDOMAIN_H__

#include <Bibim/FWD.h>
#include <Bibim/UIDomain.h>

namespace Bibim
{
    class UISimpleDomain : public UIDomain
    {
        BBModuleClass(UISimpleDomain, UIDomain, 'U', 'I', 'S', 'D');
        public:
            UISimpleDomain();
            UISimpleDomain(UIWindow* root);
            UISimpleDomain(UIWindow* root, UIVisual* focus);
            virtual ~UISimpleDomain();

            virtual UIVisual* GetFocus() const;
            virtual void SetFocus(UIVisual* value);

        protected:
            virtual void OnDestructed(UIVisual* visual);

        private:
            UIVisual* focus;
    };
}

#endif