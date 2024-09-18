using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SRCClient.Models
{
    public class Sound
    {
        public static void Play(string fileName)
        {
            try
            {
                string filePath = LoadFile.Load("Sounds", $"{fileName}.wav");

                Task.Run(() =>
                {
                    using (var audioFile = new AudioFileReader(filePath))
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        Task.Run(async () =>
                        {
                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                            {
                                await Task.Delay(10);
                            }
                        }).Wait();
                    }
                });
            }
            catch (Exception e)
            {

            }
        }
    }
}
