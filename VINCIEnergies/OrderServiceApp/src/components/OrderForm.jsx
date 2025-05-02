import { useState } from "react";
import axios from "axios";

const OrderForm = () => {
    const [order, setOrder] = useState({
        id: "",
        cliente: "",
        valor: "",
        dataPedido: "",
    });

    const handleChange = (e) => {
        setOrder({ ...order, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await axios.post("https://localhost:7077/Order", order);
            alert("Pedido cadastrado com sucesso!");
        } catch (error) {
            console.error("Erro ao cadastrar pedido", error);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input type="text" name="id" placeholder="ID do Pedido" onChange={handleChange} required />
            <input type="text" name="cliente" placeholder="Nome do Cliente" onChange={handleChange} required />
            <input type="number" name="valor" placeholder="Valor do Pedido" onChange={handleChange} required />
            <input type="date" name="dataPedido" onChange={handleChange} required />
            <button type="submit">Cadastrar Pedido</button>
        </form>
    );
};

export default OrderForm;
