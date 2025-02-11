import { Route, Routes } from "react-router-dom";
import "./App.css";
import Home from "./Pages/Home";
import Product from "./Pages/Product";

function App() {
  return (
    <>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/product" element={<Product />} />
      </Routes>
    </>
  );
}

export default App;
