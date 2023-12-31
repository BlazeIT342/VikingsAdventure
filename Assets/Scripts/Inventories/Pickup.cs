﻿using RPG.Movement;
using UnityEngine;

namespace RPG.Inventories
{
    /// <summary>
    /// To be placed at the root of a Pickup prefab. Contains the data about the
    /// pickup such as the type of item and the number.
    /// </summary>
    public class Pickup : MonoBehaviour
    {
        // STATE
        InventoryItem item;
        int number = 1;

        // CACHED REFERENCE
        Inventory inventory;
        GameObject player;

        // LIFECYCLE METHODS

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<Inventory>();
        }

        // PUBLIC

        /// <summary>
        /// Set the vital data after creating the prefab.
        /// </summary>
        /// <param name="item">The type of item this prefab represents.</param>
        /// <param name="number">The number of items represented.</param>
        public void Setup(InventoryItem item, int number)
        {
            this.item = item;
            if (!item.IsStackable())
            {
                number = 1;
            }
            this.number = number;
        }

        public InventoryItem GetItem()
        {
            return item;
        }

        public int GetNumber()
        {
            return number;
        }

        public void PickupItem()
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 3)
            {
                bool foundSlot = inventory.AddToFirstEmptySlot(item, number);
                if (foundSlot)
                {
                    player.GetComponent<Mover>().TakePickupAnim();
                    Destroy(gameObject);
                }
            }
            else
            {
                player.GetComponent<Mover>().StartMoveAction(transform.position, 1f);                
            }
        }

        public bool CanBePickedUp()
        {
            return inventory.HasSpaceFor(item);
        }
    }
}