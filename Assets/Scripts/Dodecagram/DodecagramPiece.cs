﻿using UnityEngine;
            {
                this.transform.SetParent(obj.transform);
                obj.GetComponent<BlobState>().behaviors.Switch(BlobState.states.Fleeing);
            }