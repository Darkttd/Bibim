#pragma once
#ifndef __BIBIM_CLIPBOARD_WINDOWS_H__
#define __BIBIM_CLIPBOARD_WINDOWS_H__

#include <Bibim/FWD.h>
#if (defined(BIBIM_PLATFORM_WINDOWS))

#include <Bibim/GameModule.h>
#include <Bibim/String.h>

namespace Bibim
{
    class Clipboard : public GameModule
    {
        BBModuleClass(Clipboard, GameModule, 'C', 'L', 'I', 'P');
        public:
            Clipboard();
            virtual ~Clipboard();
            
            String GetText() const;
            void SetText(const String& value);
    };
}

#endif
#endif