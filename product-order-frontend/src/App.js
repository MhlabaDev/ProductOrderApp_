import React, { useState } from "react";
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import ProductPage from "./Product/ProductPage";
import RegisterPage from "./Customer/Register";
import LoginPage from "./Customer/Login";
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  const [customer, setCustomer] = useState(null); 

  const handleLogin = (cust) => {
    setCustomer(cust);
    localStorage.setItem("customer", JSON.stringify(cust));
  };

  const handleLogout = () => {
    setCustomer(null);
    localStorage.removeItem("customer");
  };

  return (
    <Router>
      <Routes>
        {/* Always start at login */}
        <Route path="/" element={<LoginPage onLogin={handleLogin} />} />
        <Route path="/login" element={<LoginPage onLogin={handleLogin} />} />
        <Route path="/register" element={<RegisterPage onLogin={handleLogin} />} />

        {/* Products only accessible if logged in */}
        <Route
          path="/products"
          element={
            customer ? (
              <ProductPage loggedInCustomer={customer} onLogout={handleLogout} />
            ) : (
              <Navigate to="/login" />
            )
          }
        />
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </Router>
  );
}

export default App;
