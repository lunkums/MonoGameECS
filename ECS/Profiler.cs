using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ECS
{
    public class Profiler
    {
        public const int MaximumSamples = 100;

        private Stopwatch stopwatch = new();
        private Queue<float> sampleBuffer = new();

        private long previousElapsedTime = 0L;

        public long TotalFrames { get; private set; }
        public long TotalMilliseconds { get; private set; }
        public float TotalSeconds => TotalMilliseconds / 1000f;
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }

        public void Start()
        {
            stopwatch.Start();
        }

        public void Stop()
        {
            stopwatch.Stop();
            Update(stopwatch.ElapsedMilliseconds - previousElapsedTime);
            previousElapsedTime = stopwatch.ElapsedMilliseconds;
        }

        public void WriteToLog(string id)
        {
            string fileName = "MonoGameECS_" + id + "_performance_log_"
                + DateTime.Now.ToString("MM-dd-yyyy_hh-mm-ss-tt") + ".txt";
            using (StreamWriter file = File.CreateText(@"C:\dev\log\" + fileName))
            {
                JsonSerializer serializer = new();
                //serialize object directly into file stream
                serializer.Serialize(file, this);
            }
        }

        private void Update(long elapsedMilliseconds)
        {
            CurrentFramesPerSecond = 1.0f / (elapsedMilliseconds / 1000f);

            sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (sampleBuffer.Count > MaximumSamples)
            {
                sampleBuffer.Dequeue();
                AverageFramesPerSecond = sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalMilliseconds += elapsedMilliseconds;
        }
    }
}