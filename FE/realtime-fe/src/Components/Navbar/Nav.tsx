import { Link } from "react-router-dom";

const Nav = () => {
  return (
    <nav className="bg-gray-900 text-white p-4 flex justify-between items-center shadow-lg">
      <h1 className="text-2xl font-bold">My App</h1>
      <div className="space-x-4 m-auto">
        <Link to="/" className="hover:text-yellow-400 transition">
          Home
        </Link>
        <Link to="/product" className="hover:text-yellow-400 transition">
          Product
        </Link>
      </div>
    </nav>
  );
};

export default Nav;
