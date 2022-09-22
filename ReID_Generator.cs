using System;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using GTA;
using GTA.Math;
using GTA.Native;

using DWORD = System.Int32;

struct Loction
{
    public double posX;
    public double posY;
    public double posZ;
}

namespace ReID_Generator
{
    public class ReID_Gen : Script
    {
        //Initialize menu
        GTA.Menu menu_0a, menu_1a;
        //Initialize weather
        string weather = "Default";
        //Recycle ped
        Ped tmpped;

        //Random seed
        Random ran = new Random(1);

        //[SET] 카메라 좌표 설정(고도 조절은 Z 좌표 수정)
        Vector3[] pointOfView = {
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 1.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 1.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 1.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 1.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 1.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 1.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 1.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 1.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 1.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 1.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 1.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 1.0f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 1.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 1.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 1.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 1.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 1.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 1.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 1.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 1.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 1.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 1.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 1.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 1.2f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 1.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 1.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 1.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 1.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 1.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 1.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 1.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 1.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 1.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 1.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 1.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 1.4f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 1.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 1.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 1.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 1.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 1.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 1.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 1.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 1.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 1.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 1.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 1.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 1.6f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 1.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 1.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 1.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 1.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 1.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 1.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 1.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 1.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 1.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 1.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 1.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 1.8f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 2.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 2.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 2.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 2.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 2.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 2.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 2.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 2.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 2.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 2.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 2.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 2.0f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 2.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 2.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 2.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 2.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 2.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 2.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 2.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 2.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 2.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 2.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 2.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 2.2f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 2.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 2.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 2.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 2.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 2.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 2.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 2.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 2.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 2.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 2.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 2.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 2.4f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 2.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 2.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 2.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 2.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 2.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 2.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 2.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 2.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 2.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 2.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 2.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 2.6f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 2.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 2.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 2.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 2.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 2.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 2.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 2.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 2.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 2.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 2.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 2.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 2.8f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 3.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 3.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 3.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 3.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 3.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 3.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 3.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 3.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 3.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 3.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 3.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 3.0f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 3.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 3.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 3.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 3.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 3.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 3.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 3.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 3.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 3.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 3.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 3.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 3.2f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 3.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 3.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 3.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 3.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 3.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 3.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 3.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 3.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 3.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 3.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 3.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 3.4f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 3.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 3.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 3.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 3.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 3.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 3.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 3.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 3.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 3.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 3.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 3.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 3.6f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 3.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 3.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 3.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 3.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 3.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 3.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 3.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 3.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 3.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 3.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 3.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 3.8f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 4.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 4.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 4.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 4.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 4.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 4.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 4.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 4.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 4.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 4.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 4.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 4.0f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 4.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 4.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 4.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 4.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 4.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 4.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 4.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 4.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 4.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 4.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 4.2f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 4.2f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 4.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 4.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 4.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 4.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 4.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 4.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 4.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 4.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 4.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 4.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 4.4f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 4.4f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 4.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 4.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 4.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 4.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 4.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 4.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 4.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 4.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 4.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 4.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 4.6f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 4.6f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 4.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 4.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 4.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 4.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 4.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 4.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 4.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 4.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 4.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 4.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 4.8f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 4.8f},

            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 0d), Y = (float)Math.Cos(Math.PI / 18d * 0d), Z = 5.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 3d), Y = (float)Math.Cos(Math.PI / 18d * 3d), Z = 5.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 6d), Y = (float)Math.Cos(Math.PI / 18d * 6d), Z = 5.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 9d), Y = (float)Math.Cos(Math.PI / 18d * 9d), Z = 5.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 12d), Y = (float)Math.Cos(Math.PI / 18d * 12d), Z = 5.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 15d), Y = (float)Math.Cos(Math.PI / 18d * 15d), Z = 5.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 18d), Y = (float)Math.Cos(Math.PI / 18d * 18d), Z = 5.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 21d), Y = (float)Math.Cos(Math.PI / 18d * 21d), Z = 5.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 24d), Y = (float)Math.Cos(Math.PI / 18d * 24d), Z = 5.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 27d), Y = (float)Math.Cos(Math.PI / 18d * 27d), Z = 5.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 30d), Y = (float)Math.Cos(Math.PI / 18d * 30d), Z = 5.0f},
            new Vector3 { X = (float)Math.Sin(Math.PI / 18d * 33d), Y = (float)Math.Cos(Math.PI / 18d * 33d), Z = 5.0f},

        };

        Loction[] locations =
        {
            new Loction{posX = 533.268, posY = -1435.525, posZ = 29.355},
            new Loction{posX = 1244.965, posY = -1440.827, posZ = 35.053},
            new Loction{posX = 2517.980, posY = 851.251, posZ = 87.459},
        };

        //Useless ped
        int[] errHash = { 11, 12, 27, 35, 44, 54, 111, 122, 174, 175, 188, 167, 189, 210, 233, 261, 264, 310, 331, 339, 453, 406, 422, 430, 446, 474, 475, 484, 485, 509, 522, 531, 577, 581, 617, 632, 643, 666, 742 };

        //Build function
        public ReID_Gen()
        {
            GTA.MenuButton btnTest = new MenuButton("Test");
            btnTest.Activated += delegate { Test(); };
            GTA.MenuButton btnCaptureForDrone = new MenuButton("CaptureForDrone");
            btnCaptureForDrone.Activated += delegate { CaptureDataForDrone(); };
            GTA.MenuButton btnCreate = new MenuButton("Create");
            btnCreate.Activated += delegate { create(); };
            GTA.MenuButton btnCloth = new MenuButton("Cloth");
            btnCloth.Activated += delegate { setCloth(); };
            GTA.MenuButton btnWeather = new MenuButton("Weather");
            btnWeather.Activated += delegate { ChooseFunction(FunctionMode.Weather); };
            GTA.MenuButton btnHourForward = new MenuButton("Hour Forward");
            btnHourForward.Activated += delegate { HourForward(); };
            GTA.MenuButton btnHourBack = new MenuButton("Hour Back");
            btnHourBack.Activated += delegate { HourBack(); };
            menu_0a = new GTA.Menu("ReID Generator", new GTA.IMenuItem[]
            {
                btnCaptureForDrone, btnCreate, btnCloth, btnWeather,btnHourForward, btnHourBack, btnTest,
            });
            GTA.MenuButton btnBlizzard = new MenuButton("Blizzard");
            btnBlizzard.Activated += delegate { ChooseWeather(Weather.Blizzard); };
            GTA.MenuButton btnClear = new MenuButton("Clear");
            btnClear.Activated += delegate { ChooseWeather(Weather.Clear); };
            GTA.MenuButton btnClearing = new MenuButton("Clearing");
            btnClearing.Activated += delegate { ChooseWeather(Weather.Clearing); };
            GTA.MenuButton btnClouds = new MenuButton("Clouds");
            btnClouds.Activated += delegate { ChooseWeather(Weather.Clouds); };
            GTA.MenuButton btnExtrasunny = new MenuButton("Extrasunny");
            btnExtrasunny.Activated += delegate { ChooseWeather(Weather.ExtraSunny); };
            GTA.MenuButton btnFoggy = new MenuButton("Foggy");
            btnFoggy.Activated += delegate { ChooseWeather(Weather.Foggy); };
            GTA.MenuButton btnNetural = new MenuButton("Netural");
            btnNetural.Activated += delegate { ChooseWeather(Weather.Neutral); };
            GTA.MenuButton btnOvercast = new MenuButton("Overcast");
            btnOvercast.Activated += delegate { ChooseWeather(Weather.Overcast); };
            GTA.MenuButton btnSmog = new MenuButton("Smog");
            btnSmog.Activated += delegate { ChooseWeather(Weather.Smog); };
            GTA.MenuButton btnSnowlight = new MenuButton("Snowlight");
            btnSnowlight.Activated += delegate { ChooseWeather(Weather.Snowlight); };

            menu_1a = new GTA.Menu("Weather", new GTA.IMenuItem[]
            {
                btnBlizzard, btnClear, btnClearing, btnClouds, btnExtrasunny,
                btnFoggy, btnNetural, btnOvercast, btnSmog, btnSnowlight,
            });


            menu_0a.HasFooter = false;
            menu_1a.HasFooter = false;

            LeftKey = Keys.NumPad4;
            RightKey = Keys.NumPad6;
            UpKey = Keys.NumPad8;
            DownKey = Keys.NumPad2;
            ActivateKey = Keys.NumPad5;
            BackKey = Keys.NumPad0;

            this.KeyDown += OnKeyDown;
        }

        //Open main menu
        void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                View.CloseAllMenus();
                View.AddMenu(menu_0a);
            }
            if (e.KeyCode == Keys.F7)
            {
                View.CloseAllMenus();
            }
        }
        void CaptureDataForDrone()
        {   

            int n = 600;
            bool existFlag = false;
            int picNum = 0;

            while (IsFolderExist(n))
            {
                UI.ShowSubtitle(n.ToString() + "exist!", 10);
                n++;
                Wait(10);
            }
            Vector3 myPos = Game.Player.Character.Position;

            Camera cam1 = Function.Call<Camera>(Hash.CREATE_CAM, "DEFAULT_SCRIPTED_CAMERA", true);
            
            foreach (Weather weaMode in Enum.GetValues(typeof(WeatherMode)))
            {
                ChooseWeather(weaMode);
                foreach (LightMode light in Enum.GetValues(typeof(LightMode)))
                {
                    SetGameHour((int)light);
                    int angcnt = 0;
                    
                    foreach (Vector3 camAng in pointOfView)
                    {
                        string pathname = GetFoldPath((int)weaMode, (int)light, n, angcnt, ref existFlag);

                        picNum++;

                        int a = 3;

                        Vector3 camPos = Game.Player.Character.GetOffsetInWorldCoords(camAng * a);
                        Function.Call<bool>(Hash.SET_CAM_COORD, cam1.GetHashCode(), camPos.X, camPos.Y, camPos.Z);
                        Function.Call(Hash.POINT_CAM_AT_COORD, cam1.GetHashCode(), Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z);
                        Function.Call(Hash.RENDER_SCRIPT_CAMS, true, false, 0, 0, 0);
                        
                        string actorStr = Enum.GetName(typeof(PedHash), (uint)Game.Player.Character.Model.GetHashCode());
                        string location = "[" + Game.Player.Character.Position.X.ToString("0.000") + ", " + Game.Player.Character.Position.Y.ToString("0.000") + ", " + Game.Player.Character.Position.Z.ToString("0.000") + "]";
                        string time = World.CurrentDayTime.ToString();
                        string weather = this.weather;
                        string angle = "[" + (Math.Atan(camAng.Y / camAng.X)).ToString() + "]";

                        angcnt++;

                        //[SET] 화면 사이즈 설정 - 1
                        Bitmap catchBmp = new Bitmap(4096, 2160);
                        Graphics g = Graphics.FromImage(catchBmp);
                        //[SET] 화면 사이즈 설정 - 2
                        g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(4096, 2160));

                        catchBmp.Save(pathname);
                        UI.ShowSubtitle(pathname);
                        WriteToFile(angcnt.ToString(), location, time, weather, angle, actorStr);
                        Function.Call(Hash.SET_TIME_SCALE, 0.0001);
                        Wait(1);
                        
                        //Function.Call(Hash.SET_GAME_PAUSED, false);

                    }

                    
                }
            }
            
            //Convert to main camera
            Function.Call(Hash.RENDER_SCRIPT_CAMS, false, false, 0, 1, 0);
            //Destory all camera
            Function.Call(Hash.DESTROY_ALL_CAMS, true);
            Function.Call(Hash.SET_TIME_SCALE, 1.0);
        }
        //Left for test
        void Test()
        {
          
        }

        //Create specific ped
        void create()
        {
            this.tmpped = Game.Player.Character;
            Function.Call(Hash.CHANGE_PLAYER_PED, Game.Player, World.CreatePed(PedHash.Trevor, Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 5, 0))), true, true);
            tmpped.Delete();
        }

        //Change ped cloth
        void setCloth()
        {
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 100; j++)
                {
                    int drawable = ran.Next(0, 10) % 10;
                    int texture = ran.Next(0, 10) % 10;
                    if (Function.Call<bool>(Hash.IS_PED_COMPONENT_VARIATION_VALID, Game.Player.Character, i, drawable, texture))
                    {
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character, i, drawable, texture, 0);
                        break;
                    }
                }
        }

        //Change menu
        void ChooseFunction(FunctionMode funcMode)
        {
            switch (funcMode)
            {
                case FunctionMode.Weather:
                    {
                        View.CloseAllMenus();
                        View.AddMenu(menu_1a);
                        break;
                    }
            }
        }

        //Choose weather
        void ChooseWeather(Weather weaMode)
        {
            if((int)weaMode == -1)
            {

            }
            else if (((int)weaMode == 1) | ((int)weaMode == 5) | ((int)weaMode == 6) | ((int)weaMode == 7) | ((int)weaMode == 8) | ((int)weaMode == 10) | ((int)weaMode == 13))
            {

            }
            else
            {
                Function.Call(Hash.SET_WEATHER_TYPE_NOW_PERSIST, Enum.GetName(typeof(Weather), weaMode));
                this.weather = Enum.GetName(typeof(Weather), weaMode);
            }
        }

        //Set time forward
        void HourForward()
        {
            int h = World.CurrentDayTime.Hours;
            int m = World.CurrentDayTime.Minutes;
            if (h == 23) { h = 0; }
            else { h++; }
            Function.Call(Hash.SET_CLOCK_TIME, h, m, 0);
            UI.ShowSubtitle("Set time to " + World.CurrentDayTime.ToString());
        }

        //Set time
        void SetGameHour(int hour)
        {
            Function.Call(Hash.SET_CLOCK_TIME, hour, 0, 0);
            UI.ShowSubtitle("Set time to " + World.CurrentDayTime.ToString());
        }

        //Set time back
        void HourBack()
        {
            int h = World.CurrentDayTime.Hours;
            int m = World.CurrentDayTime.Minutes;
            if (h == 0) { h = 23; }
            else { h--; }
            Function.Call(Hash.SET_CLOCK_TIME, h, m, 0);
            UI.ShowSubtitle("Set time to " + World.CurrentDayTime.ToString());
        }

        //Write log
        void WriteToFile(string user, string location, string time, string weather, string angle, string actorped)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(@".\scripts\ReID_Generator_log.txt", true))
                {
                    string datetimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
                    string logStr = "[" + datetimeStr + "] (" + user + ") " + "Loc:{" + location + "} Time:{" + time + "} Weather:{" + weather + "} Ang:{" + angle + "} Actor:{" + actorped + "}";
                    sw.WriteLine(logStr);
                }
            }
            catch
            {
                UI.Notify("Failed to save!");
            }
        }

        //Get ID save path
        string GetFoldPath(int wea, int light, int n, int angcnt, ref bool exist)
        {
            //[SET] 이미지 저장 경로 설정 - 1
            string folderpath = "D:\\IMG_new\\" + n.ToString().PadLeft(4, '0');
            if (false == System.IO.Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
                exist = false;
                UI.ShowSubtitle(folderpath);
            }
            else
            {
                exist = true;
            }
            string weaCode, lightCode, foldpath;
            switch (wea)
            {
                case 0:
                    weaCode = "-1";
                    break;
                case 1:
                    weaCode = "00";
                    break;
                case 2:
                    weaCode = "01";
                    break;
                case 3:
                    weaCode = "02";
                    break;
                case 4:
                    weaCode = "03";
                    break;
                case 9:
                    weaCode = "04";
                    break;
                case 11:
                    weaCode = "05";
                    break;
                case 12:
                    weaCode = "06";
                    break;
                default:
                    weaCode = "00";
                    break;
            }
            switch (light)
            {
                case 0:
                    lightCode = "00";
                    break;
                case 5:
                    lightCode = "01";
                    break;
                case 8:
                    lightCode = "02";
                    break;
                case 12:
                    lightCode = "03";
                    break;
                case 18:
                    lightCode = "04";
                    break;
                case 19:
                    lightCode = "05";
                    break;
                case 20:
                    lightCode = "06";
                    break;
                default:
                    lightCode = "00";
                    break;
            }
            foldpath = folderpath + "\\" + n.ToString().PadLeft(4, '0') + "_C" + angcnt.ToString().PadLeft(2, '0') + "_W" + weaCode + "_L" + lightCode +  ".jpg";
            return foldpath;
        }

        //Check if folder exist
        bool IsFolderExist(int n)
        {
            //[SET] 이미지 저장 경로 설정 - 2
            string folderpath = "D:\\IMG_new\\" + n.ToString().PadLeft(4, '0');
            if (false == System.IO.Directory.Exists(folderpath))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        enum FunctionMode
        {
            Appearance, Weather,
        }

        enum AppearMode
        {
            Character, Cloth, Skin, Hair, Shape,
        }

        enum WeatherMode
        {
            ExtraSunny = 0,
            Clear = 1,
            Clouds = 2,
            Smog = 3,
            Foggy = 4,
            Neutral = 9,
            Blizzard = 11,
            Snowlight = 12,
        }

        enum LightMode
        {
            Midnight = 0,
            Dawn = 5,
            Forenoon = 8,
            Noon = 12,
            Afternoon = 18,
            Dusk = 19,
            Night = 20,
        }
    }
}

