import Nav from "../Components/Navbar/Nav";

const Product = () => {
  return (
    <div>
      <Nav />
    <div className="flex flex-col items-center h-screen bg-gray-100 p-10">
      <h2 className="text-3xl font-bold text-gray-800 mb-6">Product List</h2>
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
        <div className="bg-white p-6 rounded-lg shadow-lg">
          <h3 className="text-xl font-semibold">Product 1</h3>
          <p className="text-gray-600">Description of product 1</p>
        </div>
        <div className="bg-white p-6 rounded-lg shadow-lg">
          <h3 className="text-xl font-semibold">Product 2</h3>
          <p className="text-gray-600">Description of product 2</p>
        </div>
        <div className="bg-white p-6 rounded-lg shadow-lg">
          <h3 className="text-xl font-semibold">Product 3</h3>
          <p className="text-gray-600">Description of product 3</p>
        </div>
      </div>
    </div>
    </div>
  );
};

export default Product;
