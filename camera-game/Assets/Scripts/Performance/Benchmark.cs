using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseOperations
{
    [Flags]
    public enum MouseEventFlags
    {
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        MiddleDown = 0x00000020,
        MiddleUp = 0x00000040,
        Move = 0x00000001,
        Absolute = 0x00008000,
        RightDown = 0x00000008,
        RightUp = 0x00000010
    }

    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePoint lpMousePoint);

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    public static void SetCursorPosition(int x, int y)
    {
        SetCursorPos(x, y);
    }

    public static void SetCursorPosition(MousePoint point)
    {
        SetCursorPos(point.X, point.Y);
    }

    public static MousePoint GetCursorPosition()
    {
        MousePoint currentMousePoint;
        var gotPoint = GetCursorPos(out currentMousePoint);
        if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
        return currentMousePoint;
    }

    public static void MouseEvent(MouseEventFlags value)
    {
        MousePoint position = GetCursorPosition();

        mouse_event
            ((int)value,
             position.X,
             position.Y,
             0,
             0)
            ;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint
    {
        public int X;
        public int Y;

        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}


public class Benchmark : MonoBehaviour
{
    public class Frame {
        public MouseOperations.MousePoint mousePoint;
        public List<MouseOperations.MouseEventFlags> mouseEvents = new List<MouseOperations.MouseEventFlags>();
    }

    public List<Frame> frames = new List<Frame>();
    public bool recording = false;

    private void Start()
    {
        StartCoroutine(StopRecording());
    }

    IEnumerator StopRecording()
    {
        yield return new WaitForSeconds(5);
        recording = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (recording)
        {
            Record();
        }
        else
        {
            Play();
        }
    }

    void Record()
    {
        Frame f = new Frame();

        f.mousePoint = MouseOperations.GetCursorPosition();


        if (Input.GetMouseButtonDown(0))
        {
            f.mouseEvents.Add(MouseOperations.MouseEventFlags.LeftDown);
        }

        if (Input.GetMouseButtonUp(0))
        {
            f.mouseEvents.Add(MouseOperations.MouseEventFlags.LeftUp);
        }

        if (Input.GetMouseButtonDown(1))
        {
            f.mouseEvents.Add(MouseOperations.MouseEventFlags.RightDown);
        }

        if (Input.GetMouseButtonUp(1))
        {
            f.mouseEvents.Add(MouseOperations.MouseEventFlags.RightUp);
        }

        if (Input.GetMouseButtonDown(2))
        {
            f.mouseEvents.Add(MouseOperations.MouseEventFlags.MiddleDown);
        }

        if (Input.GetMouseButtonUp(2))
        {
            f.mouseEvents.Add(MouseOperations.MouseEventFlags.MiddleUp);
        }

        frames.Add(f);
    }

    void Play()
    {
        if (frames.Count == 0) return;

        Frame f = frames[0];
        MouseOperations.SetCursorPosition(f.mousePoint);

        foreach (MouseOperations.MouseEventFlags mouseEvent in f.mouseEvents)
        {
            MouseOperations.MouseEvent(mouseEvent);
        }

        frames.RemoveAt(0);
    }
}
