import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "../index.css";

const Register = () => {
  const navigate = useNavigate();
  const [form, setForm] = useState({
    firstName: "",
    surname: "",
    addressType: "Residential",
    streetAddress: "",
    suburb: "",
    city: "",
    postalCode: "",
  });
  const [error, setError] = useState("");

  const handleChange = (e) =>
    setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch("http://localhost:5225/api/Customer", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(form),
      });

      if (!response.ok) throw new Error("Failed to register");

      alert("Registration successful!");
      navigate("/login");
    } catch (err) {
      console.error(err);
      setError("Registration failed. Please check your input.");
    }
  };

  return (
    <div className="login-register-container">
      <div className="login-register-card">
        <h2>Register</h2>
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
          <select name="addressType" value={form.addressType} onChange={handleChange}>
            <option>Residential</option>
            <option>Business</option>
          </select>
          <input
            name="streetAddress"
            placeholder="Street Address"
            value={form.streetAddress}
            onChange={handleChange}
            required
          />
          <input
            name="suburb"
            placeholder="Suburb"
            value={form.suburb}
            onChange={handleChange}
          />
          <input
            name="city"
            placeholder="City"
            value={form.city}
            onChange={handleChange}
            required
          />
          <input
            name="postalCode"
            placeholder="Postal Code"
            value={form.postalCode}
            onChange={handleChange}
            required
          />
          {error && <p style={{ color: "red" }}>{error}</p>}
          <button type="submit">Register</button>
        </form>
        <p>
          Already have an account? <span onClick={() => navigate("/login")}>Login</span>
        </p>
      </div>
    </div>
  );
};

export default Register;
