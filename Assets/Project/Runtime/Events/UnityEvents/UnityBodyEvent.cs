﻿using System;
using Realm;
using UnityEngine.Events;

namespace Events
{
    [Serializable] public class UnityBodyEvent : UnityEvent<BodyType> { }
}