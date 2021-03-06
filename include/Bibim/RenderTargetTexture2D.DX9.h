﻿#pragma once
#ifndef __BIBIM_RENDERTARGETTEXTURE2D_DX9_H__
#define __BIBIM_RENDERTARGETTEXTURE2D_DX9_H__

#include <Bibim/FWD.h>
#if (defined(BIBIM_USE_DIRECTX9))

#include <Bibim/Texture2D.DX9.h>
#include <Bibim/Rect.h>

namespace Bibim
{
    class RenderTargetTexture2D : public Texture2D
    {
        BBAssetClass(RenderTargetTexture2D, Texture2D, 'R', 'T', 'X', '2');
        public:
            RenderTargetTexture2D(GraphicsDevice* graphicsDevice, int width, int height);
            virtual ~RenderTargetTexture2D();

            inline IDirect3DSurface9* GetD3DSurface() const;

        private:
            virtual void OnGraphicsDeviceLost(GraphicsDeviceBase* g);

        private:
            IDirect3DSurface9* d3dSurface;
    };

    ////////////////////////////////////////////////////////////////////////////////////////////////////

    IDirect3DSurface9* RenderTargetTexture2D::GetD3DSurface() const
    {
        return d3dSurface;
    }
}

#endif
#endif