import { useState } from "react";
import axios from "axios";

const OrderList = () => {
    const [orderId, setOrderId] = useState("");
    const [order, setOrder] = useState(null);

    const fetchOrder = async () => {
        try {
            const response = await axios.get(`https://localhost:7077/Order/${orderId}`);
            setOrder(response.data);
        } catch (error) {
            console.error("Erro ao buscar pedido", error);
            setOrder(null);
        }
    };

    return (
        <div>
            <input type="text" placeholder="ID do Pedido" onChange={(e) => setOrderId(e.target.value)} />
            <button onClick={fetchOrder}>Buscar Pedido</button>
            {order && (
                <div>
                    <h3>Detalhes do Pedido</h3>
                    <p><strong>ID:</strong> {order.id}</p>
                    <p><strong>Cliente:</strong> {order.cliente}</p>
                    <p><strong>Valor:</strong> {order.valor}</p>
                    <p><strong>Data:</strong> {order.dataPedido}</p>
                </div>
            )}
        </div>
    );
};

export default OrderList;
