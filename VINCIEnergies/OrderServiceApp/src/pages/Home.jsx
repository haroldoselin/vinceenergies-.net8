import OrderForm from "../components/OrderForm";
import OrderList from "../components/OrderList";

const Home = () => {
    return (
        <div>
            <h1>Gerenciamento de Pedidos</h1>
            <OrderForm />
            <OrderList />
        </div>
    );
};

export default Home;
