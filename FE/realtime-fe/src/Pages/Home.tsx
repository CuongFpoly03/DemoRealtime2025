import { useEffect } from "react";
import Nav from "../Components/Navbar/Nav";
import connection from "../Components/Hubs/Signalr";

const Home = () => {
  useEffect(() => {
    connection.on("ReceiveNotification", (message) => {
      alert("Thông báo: " + message);
    });

    return () => {
      connection.off("ReceiveNotification");
    };
  }, []);
  return (
    <div>
      <Nav />
      <div className="flex justify-center items-center h-screen bg-gradient-to-r from-blue-400 to-purple-500">
        <h2 className="text-white text-4xl font-semibold">
          Welcome to Home Page
        </h2>
      </div>
    </div>
  );
};

export default Home;
