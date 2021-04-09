using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CostData {
    public CostRequirement[] resources = new CostRequirement[1];
    public CostData () { }

    public CostData (CostData data) {
        for (int i = 0; i < data.resources.Length; i++) {
            resources[0] = data.resources[0];
        }
    }

    public CostData (CostRequirement[] resources) {
        this.resources = resources;
    }

    [System.Serializable]
    public class CostRequirement {
        public ResourcesItem resourcesData;
        public int resourcesAmount = -1;

        public CostRequirement () { }

        public bool isEnough (CostRequirement inputValue) {
            if (inputValue.resourcesData == null) {
                return true;
            }

            if (inputValue.resourcesData == resourcesData) {
                return inputValue.resourcesAmount >= resourcesAmount;
            }
            return false;
        }
    }
}