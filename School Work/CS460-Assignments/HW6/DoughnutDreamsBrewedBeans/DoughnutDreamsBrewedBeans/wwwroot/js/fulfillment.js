document.addEventListener('DOMContentLoaded', initializePage, false);

function initializePage() {
    console.log("Page loaded, initializing javascript");
    const stationSelect = document.getElementById("station-select");
    stationSelect.addEventListener('change', getOrders, false);
    getOrders();
    setInterval(getOrders, 10000);
}

async function getOrders() {
    const url = "/api/DDBB/orders/station/";
    const station = document.getElementById("station-select").value;
    const noOrders = document.getElementById("no-orders");
    const ordersDiv = document.getElementById("orders-div");
    if (station === "0")
    {
        noOrders.textContent = "Select a Station";
        ordersDiv.textContent = "";
        return;
    }
    console.log("Getting orders for station " + station);
    try {
        const response = await fetch(url + station);
        if (response.status !== 200) {
            if (response.status === 204)
            {
                noOrders.textContent = "No Current Orders for this Station";
                ordersDiv.textContent = "";
                return;
            }
            noOrders.textContent = "Something went wrong";
            return;
        }
        noOrders.textContent = "";
        const orders = await response.json();
        console.log(orders);
        ordersDiv.textContent = "";
        const orderTemplate = document.getElementById("order-template");
        const itemTemplate = document.getElementById("item-template");
        if (orders.length === 0) {
            noOrders.textContent = "No Current Orders for this Station";
            ordersDiv.textContent = "";
            return;
        }
        orders.forEach(order => {
            const clone = orderTemplate.content.cloneNode(true);
            const orderItemTable = clone.querySelector("#item-table");
            const orderId = clone.querySelector("#order-id");
            const orderName = clone.querySelector("#order-name");
            const orderStore = clone.querySelector("#order-store");
            orderId.textContent = `Order #${order.id}`;
            orderName.textContent = `Name: ${order.customerName} @ ${order.deliveryLocation}`;
            orderStore.textContent = order.store;
            order.orderItems.forEach(item => {
                const itemClone = itemTemplate.content.cloneNode(true);
                const itemName = itemClone.querySelector("#item-name");
                const itemQuantity = itemClone.querySelector("#item-quantity");
                const itemDesc = itemClone.querySelector("#item-desc");
                const itemStatus = itemClone.querySelector("#item-status");
                itemName.textContent = item.itemName;
                itemQuantity.textContent = item.quantity;
                if (item.status === "In Progress")
                {
                    itemStatus.innerHTML = `<button type="button" class="btn-status btn-incomplete"></button>`;
                }
                else
                {
                    itemStatus.innerHTML = `<button type="button" class="btn-status btn-complete"></button>`;
                }
                itemDesc.textContent = item.description;
                itemStatus.addEventListener('click', () => updateOrder(item.id), false)
                orderItemTable.appendChild(itemClone);
            })
        ordersDiv.appendChild(clone);
        })
    }
    catch (err) {
        noOrders.textContent = "Something went wrong";
        ordersDiv.textContent = "";
        return;
    }
}

async function updateOrder(id)
{
    const url = `/api/DDBB/orders/${id}`;
    const response = await fetch(url, {
        method: 'PUT'
    });
    if (response.status !== 200 && response.status !== 204 && response.status !== 201) {
        return;
    }
    else
    {
        getOrders();
    }
}