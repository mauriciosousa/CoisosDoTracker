using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;

public class SurfaceRequestListener : MonoBehaviour
{
    public int listenPort;

    private UdpClient _udpClient_LocalSurface = null;
    private IPEndPoint _anyIP_LocalSurface;

    private SurfaceRectangle _surface = null;

    public SurfaceCalib surfaceCalib;

    void Awake()
    {
        StartReceive();
    }

    public void StartReceive()
    {
        _anyIP_LocalSurface = new IPEndPoint(IPAddress.Any, listenPort);
        _udpClient_LocalSurface = new UdpClient(_anyIP_LocalSurface);
        _udpClient_LocalSurface.BeginReceive(new AsyncCallback(this.ReceiveCallback_LocalSurface), null);

        gameObject.GetComponent<SurfaceRequest>().request();

    }

    public void ReceiveCallback_LocalSurface(IAsyncResult ar)
    {
        Byte[] receiveBytes = _udpClient_LocalSurface.EndReceive(ar, ref _anyIP_LocalSurface);
        string result = System.Text.Encoding.UTF8.GetString(receiveBytes);

        if (SurfaceMessage.isMessage(result))
        {
            _surface = new SurfaceRectangle(result);

            Debug.Log(_surface.ToString());

            _udpClient_LocalSurface.Close();
        }
        else
            _udpClient_LocalSurface.BeginReceive(new AsyncCallback(this.ReceiveCallback_LocalSurface), null);
    }

    void Update ()
    {
        if (_surface != null)
        {
            //gameObject.GetComponent<TrackerClient>().setSurface(_surface);
            surfaceCalib.calibrate(_surface);
            _surface = null;
        }
    }

    void OnApplicationQuit()
    {
        if (_udpClient_LocalSurface != null) _udpClient_LocalSurface.Close();
    }

    void OnQuit()
    {
        OnApplicationQuit();
    }
}
