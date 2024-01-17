document.addEventListener('DOMContentLoaded', initializePage, false);

function initializePage() {
    console.log("Page loaded, initializing javascript");
    getOrders();
    setInterval(getOrders, 10000);
}

async function getOrders() {
    console.log("Getting orders");
    const ordersDiv = document.getElementById("orders-div");
    const noOrders = document.getElementById("no-orders");
    try {
        const response = await fetch("/api/DDBB/orders");
        if (response.status !== 200) {
            if (response.status === 204)
            {
                noOrders.textContent = "No Current Orders";
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
            noOrders.textContent = "No Current Orders";
            ordersDiv.textContent = "";
            return;
        }
        orders.forEach(order => {
            const clone = orderTemplate.content.cloneNode(true);
            const orderItemTable = clone.querySelector("#item-table");
            const orderId = clone.querySelector("#order-id");
            const orderName = clone.querySelector("#order-name");
            const orderTotal = clone.querySelector("#order-total");
            const orderStatus = clone.querySelector("#order-status");
            const orderDlvy = clone.querySelector("#order-dlvy");
            const orderStore = clone.querySelector("#order-store");
            orderId.textContent = `Order #${order.id}`;
            orderName.textContent = `Name: ${order.customerName}`;
            if (order.complete === "true") {
                orderStatus.textContent = "Status: Complete";
            }
            else {
                orderStatus.textContent = "Status: In Progress";
            }
            orderTotal.textContent = `Total: ${order.totalPrice}`;
            orderDlvy.textContent = `@ ${order.deliveryLocation}`;
            orderStore.textContent = order.store;

            order.orderItems.forEach(item => {
                const itemClone = itemTemplate.content.cloneNode(true);
                const stationName = itemClone.querySelector("#station-name");
                const itemName = itemClone.querySelector("#item-name");
                const itemQuantity = itemClone.querySelector("#item-quantity");
                const itemStatus = itemClone.querySelector("#item-status");
                stationName.textContent = item.stationName;
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