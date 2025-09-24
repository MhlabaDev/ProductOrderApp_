import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "../index.css";

const Login = ({ onLogin }) => {
  const navigate = useNavigate();
  const [form, setForm] = useState({ firstName: "", surname: "" });
  const [customers, setCustomers] = useState([]);
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchCustomers = async () => {
      try {
        const response = await fetch("http://localhost:5225/api/Customer");
        const data = await response.json();
        
        /// <summary>
        /// Handle .NET JSON responses
        /// </summary>
        setCustomers(Array.isArray(data) ? data : data.$values || []);
      } catch (err) {
        console.error("Error fetching customers:", err);
      }
    };
    fetchCustomers();
  }, []);

  const handleChange = (e) =>
    setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = (e) => {
    e.preventDefault();
    const foundCustomer = customers.find(
      (c) => c.firstName === form.firstName && c.surname === form.surname
    );

    if (foundCustomer) {
      if (onLogin) onLogin(foundCustomer);
      navigate("/products");
    } else {
      setError("Customer not found. Please register or check your details.");
    }
  };

  return (
    <div className="login-register-container">
      <div className="login-register-card">
        <h2>Login</h2>
        <form onSubmit={handleSubmit}>
          <input
            name="firstName"
            placeholder="First Name"
            value={form.firstName}
            onChange={handleChange}
            required
          />
          <input
            name="surname"
            placeholder="Surname"
            value={form.surname}
            onChange={handleChange}
            required
          />
          {error && <p style={{ color: "red" }}>{error}</p>}
          <button type="submit">Login</button>
        </form>
        <p>
          Don't have an account?{" "}
          <span onClick={() => navigate("/register")}>Register</span>
        </p>
      </div>
    </div>
  );
};

export default Login;
