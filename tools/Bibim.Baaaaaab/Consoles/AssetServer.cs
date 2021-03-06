﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Bibim;
using Bibim.Asset;
using Bibim.Asset.Pipeline;
using Bibim.Asset.Pipeline.Recipes;
using Bibim.IO;
using Bibim.Toolkit;

using Bibim.Animation;

namespace Bibim.Bab.Consoles
{
    [ConsoleEntryPoint]
    [Alias("assetsvr")]
    public static class AssetServer
    {
        static void Run()
        {
            Trace.WriteLine("================================");
            Trace.WriteLine("Halak Bibim Asset Server");
            Trace.WriteLine("================================");
            Trace.WriteLine(string.Format("Listen Port: {0}", NetworkAssetProvider.DefaultPort));

            var modules = new GameModuleTree();
            var assetStorage = new GameAssetStorage();
            var assetKitchen = new GameAssetKitchen(assetStorage);
            var assetServer = new NetworkGameAssetServer(assetKitchen);
            modules.Root.AttachChild(assetStorage);
            modules.Root.AttachChild(assetKitchen);
            modules.Root.AttachChild(assetServer);

            bool closed = false;
            while (closed == false)
            {
                string command;
                if (ConsoleWindow.CommandQueue.TryDequeue(out command))
                {
                    switch (command.ToLower())
                    {
                        case "exit":
                            closed = true;
                            break;
                    }

                    if (command.StartsWith("deletecache "))
                    {
                        int numberOfDeletedFiles = 0;
                        string path = command.Substring("deletecache ".Length).Trim();
                        try
                        {
                            string[] files = Directory.GetFiles(path, "*." + GameAsset.BinaryFileExtension, SearchOption.AllDirectories);
                            foreach (string item in files)
                            {
                                try
                                {
                                    File.Delete(item);
                                    numberOfDeletedFiles++;
                                }
                                catch (Exception ex)
                                {
                                    Trace.WriteLine(ex);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex);
                        }

                        Trace.TraceInformation("Asset cache deleted ({0}).", numberOfDeletedFiles);
                    }

                    if (command.StartsWith("precook "))
                    {
                        int index1 = "precook ".Length;
                        int index2 = command.IndexOf(">>", index1);
                        if (index2 == -1)
                            Precook(assetServer, command.Substring(index1).Trim());
                        else
                        {
                            Precook(assetServer,
                                    command.Substring(index1, index2 - index1).Trim(),
                                    command.Substring(index2 + ">>".Length).Trim());
                        }
                    }

                    if (command.StartsWith("compress "))
                    {
                        int index1 = "compress ".Length;
                        int index2 = command.IndexOf(">>", index1);

                        string directory = command.Substring(index1, index2 - index1).Trim();
                        string zipFilePath = Path.ChangeExtension(command.Substring(index2 + ">>".Length), "zip").Trim();
                        Compress(directory, zipFilePath);
                    }


                    if (command.StartsWith("precook_and_compress "))
                    {
                        int index1 = "precook_and_compress ".Length;
                        int index2 = command.IndexOf(">>", index1);

                        string directory = command.Substring(index1, index2 - index1).Trim();
                        string zipFilePath = Path.ChangeExtension(command.Substring(index2 + ">>".Length), "zip").Trim();
                        Precook(assetServer, directory);
                        Compress(directory, zipFilePath);
                    }
                }
                else
                    System.Threading.Thread.Sleep(10);
            }

            assetServer.Alive = false;
            Trace.TraceInformation("AssetServer closed.");
        }

        private static void Precook(GameAssetServer assetServer, string path)
        {
            try
            {
                string[] files = Directory.GetFiles(path, "*." + GameAsset.TextFileExtension, SearchOption.AllDirectories);
                int countingIndex = files.Length / 10;
                for (int i = 0, k = 0; i < files.Length; i++, k++)
                {
                    try
                    {
                        string assetPath = files[i].Substring(path.Length + 1);

                        if (i == files.Length - 1)
                        {
                            assetServer.Precook(path, assetPath, (_, __, ___) => { Trace.TraceInformation("LAST COOKING!"); }, () => { Trace.TraceInformation("LAST COOKING!"); });
                        }
                        else if (k == countingIndex)
                        {
                            k = 0;
                            int count = i;
                            assetServer.Precook(path, assetPath, (_, __, ___) => { Trace.TraceInformation("({0}/{1})", count, files.Length); }, () => { Trace.TraceInformation("({0}/{1})", count, files.Length); });
                        }
                        else
                        {
                            assetServer.Precook(path, assetPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            while (assetServer.IsBusy)
                Thread.Sleep(100);
        }

        private static void Precook(GameAssetServer assetServer, string directory, string assetPath)
        {
            try
            {
                assetServer.Precook(directory, assetPath);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            while (assetServer.IsBusy)
                Thread.Sleep(100);
        }

        private static void Compress(string directory, string zipFilePath)
        {
            using (var zipFile = new Ionic.Zip.ZipFile())
            {
                string[] files = null;

                Action<string> collect = (extension) =>
                {
                    files = Directory.GetFiles(directory, "*." + extension, SearchOption.AllDirectories);
                    foreach (string item in files)
                        zipFile.AddFile(item, Path.GetDirectoryName(item.Substring(directory.Length + 1)));
                };

                collect(GameAsset.BinaryFileExtension);
                collect("lua");
                collect("ogg");
                collect("wav");
                collect("mp3");
                collect("ttf");

                Trace.WriteLine("Begin ZIP Creation");
                zipFile.Save(Path.Combine(directory, zipFilePath));
            }

            Trace.TraceInformation("ZIP FILE Created! {0}", zipFilePath);
        }
    }
}
