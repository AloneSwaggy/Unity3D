using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OrderEventArgs : EventArgs
{
    public Order Order { get; private set; }

    public OrderEventArgs(Order order)
    {
        Order = order;
    }
}