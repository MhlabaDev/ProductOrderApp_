import React, { useRef, useEffect, useState } from "react";
import "../index.css";
import { getProducts, createOrder, showNotification } from "../ProductService";

function ProductPage({ loggedInCustomer, onLogout }) {
  const navbarRef = useRef(null);
  const [navbarHeight, setNavbarHeight] = useState(0);
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [searchTerm, setSearchTerm] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const productsPerPage = 4;

  const [cartVisible, setCartVisible] = useState(false);
  const [cartItems, setCartItems] = useState([]);
  const [orderConfirmation, setOrderConfirmation] = useState(null);
  const [notification, setNotification] = useState(null);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const data = await getProducts();
        if (data?.$values) setProducts(data.$values);
        else if (Array.isArray(data)) setProducts(data);
        else setProducts([]);
      } catch {
        setError("Failed to fetch products.");
      } finally {
        setLoading(false);
      }
    };
    fetchProducts();
  }, []);

  // Set navbar height & restore cart
  useEffect(() => {
    if (navbarRef.current) setNavbarHeight(navbarRef.current.offsetHeight);
    const savedCart = JSON.parse(localStorage.getItem("cart")) || [];
    setCartItems(savedCart);
  }, []);


  const handleAddToCart = (product) => {
    const updatedCart = [...cartItems];
    const existing = updatedCart.find(p => p.productId === product.productId);
    if (existing) existing.quantity += 1;
    else updatedCart.push({ ...product, quantity: 1 });
    setCartItems(updatedCart);
    localStorage.setItem("cart", JSON.stringify(updatedCart));
    showNotification(`${product.title} has been added to cart`);
  };


  const handleQuantityChange = (productId, delta) => {
    const updatedCart = cartItems.map(item =>
      item.productId === productId
        ? { ...item, quantity: Math.max(1, item.quantity + delta) }
        : item
    );
    setCartItems(updatedCart);
    localStorage.setItem("cart", JSON.stringify(updatedCart));
  };


  const handleRemove = (productId) => {
    const updatedCart = cartItems.filter(item => item.productId !== productId);
    setCartItems(updatedCart);
    localStorage.setItem("cart", JSON.stringify(updatedCart));
  };


  const cartTotal = cartItems.reduce((sum, item) => sum + item.price * item.quantity, 0);

  const handleCheckout = async () => {
    if (cartItems.length === 0) {
      showNotification("Your cart is empty!", "error");
      return;
    }

    try {
      const orderItems = cartItems.map(item => ({
        orderItemId: 0,
        orderId: 0,
        productId: item.productId,
        quantity: item.quantity,
        unitPrice: item.price,
        product: {
          productId: item.productId,
          title: item.title,
          description: item.description,
          price: item.price,
          imageUrl: item.imageUrl
        }
      }));

      const orderPayload = {
        orderId: 0,
        customerId: loggedInCustomer.id,
        customer: loggedInCustomer,
        createdAt: new Date().toISOString(),
        items: orderItems
      };

      const savedOrder = await createOrder(orderPayload);

      setOrderConfirmation({
        orderId: savedOrder.orderId,
        total: cartTotal.toFixed(2),
        items: cartItems
      });

      setCartItems([]);
      localStorage.removeItem("cart");
      setCartVisible(false);

      showNotification("Order has been successfully placed!");
    } catch (err) {
      console.error(err);
      showNotification("Failed to process the order.", "error");
    }
  };

  const filteredProducts = products.filter(p =>
    p.title.toLowerCase().includes(searchTerm.toLowerCase())
  );
  const indexOfLast = currentPage * productsPerPage;
  const indexOfFirst = indexOfLast - productsPerPage;
  const currentProducts = filteredProducts.slice(indexOfFirst, indexOfLast);
  const totalPages = Math.ceil(filteredProducts.length / productsPerPage);

  if (!loggedInCustomer) {
    return (
      <div style={{ padding: "2rem", textAlign: "center" }}>
        <h2>Please login to see products</h2>
        <p>
          Go to <a href="/login">Login</a> or <a href="/register">Register</a>
        </p>
      </div>
    );
  }

  return (
    <div className="product-page">
      {/* Notification */}
      {notification && (
        <div className={`notification ${notification.type}`} style={{ top: navbarHeight + 10 }}>
          {notification.message}
        </div>
      )}

      {/* Navbar */}
      <header ref={navbarRef} className="navbar">
        <a href="/" className="navbar-brand">MyStore</a>

        <nav className="navbar-nav">
          {loggedInCustomer && (
            <span className="navbar-welcome">Hello, {loggedInCustomer.firstName}</span>
          )}

          <div className="navbar-buttons">
            <button className="cart-btn" onClick={() => setCartVisible(!cartVisible)}>
              🛒 Cart ({cartItems.length})
            </button>
            {loggedInCustomer && (
              <button
                className="logout-btn"
                onClick={() => {
                  onLogout();
                  localStorage.removeItem("customer");
                }}
              >
                Logout
              </button>
            )}
          </div>
        </nav>
      </header>

      {/* Product grid */}
      <main className="product-grid-wrapper" style={{ paddingTop: navbarHeight }}>
        {orderConfirmation ? (
          <div className="order-confirmation-card-top">
            <h2>✅ Order Successful!</h2>
            <p>Order ID: {orderConfirmation.orderId}</p>
            <p>Total: R {orderConfirmation.total}</p>
            <h4>Items:</h4>
            <div className="order-items-grid">
              {orderConfirmation.items.map(item => (
                <div key={item.productId} className="order-item-card">
                  <img src={item.imageUrl} alt={item.title} className="order-item-image" />
                  <span>{item.quantity} × {item.title}</span>
                  <span>R {item.price.toFixed(2)} each</span>
                </div>
              ))}
            </div>
            <button className="back-btn" onClick={() => setOrderConfirmation(null)}>Back to Products</button>
          </div>
        ) : (
          <>
            <input
              type="text"
              placeholder="Search products..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="product-search"
            />
            {error && <p className="error-text">{error}</p>}

            <div className="product-grid">
              {loading
                ? Array.from({ length: productsPerPage }).map((_, i) => (
                  <div key={i} className="product-card shimmer">
                    <div className="product-image shimmer-box"></div>
                    <div className="product-info">
                      <div className="shimmer-box title"></div>
                      <div className="shimmer-box desc"></div>
                      <div className="shimmer-box price"></div>
                    </div>
                  </div>
                ))
                : currentProducts.map(product => (
                  <div key={product.productId} className="product-card">
                    <img src={product.imageUrl} alt={product.title} className="product-image" />
                    <div className="product-info">
                      <h5 className="product-title">{product.title}</h5>
                      <p className="product-description">{product.description}</p>
                      <div className="product-footer">
                        <span className="product-price">R {product.price.toFixed(2)}</span>
                        <button className="add-to-cart-btn" onClick={() => handleAddToCart(product)}>Add to Cart</button>
                      </div>
                    </div>
                  </div>
                ))}
              {currentProducts.length % 4 !== 0 &&
                Array.from({ length: 4 - (currentProducts.length % 4) }).map((_, i) => (
                  <div key={`empty-${i}`} className="product-card empty-card"></div>
                ))}
            </div>

            <div className="pagination">
              {Array.from({ length: totalPages }, (_, i) => (
                <button
                  key={i}
                  onClick={() => setCurrentPage(i + 1)}
                  className={currentPage === i + 1 ? "active" : ""}
                >
                  {i + 1}
                </button>
              ))}
            </div>
          </>
        )}
      </main>

      {/* Cart overlay */}
      {cartVisible && (
        <div className="cart-overlay">
          <h3>Your Cart</h3>
          {cartItems.length === 0 ? (
            <p>Your cart is empty</p>
          ) : (
            <>
              {cartItems.map(item => (
                <div key={item.productId} className="cart-item">
                  <img src={item.imageUrl} alt={item.title} />
                  <div className="cart-item-info">
                    <span className="cart-item-title">{item.title}</span>
                    <div className="cart-item-quantity">
                      <button onClick={() => handleQuantityChange(item.productId, -1)}>-</button>
                      <span>{item.quantity}</span>
                      <button onClick={() => handleQuantityChange(item.productId, 1)}>+</button>
                    </div>
                  </div>
                  <span className="cart-item-price">R {(item.price * item.quantity).toFixed(2)}</span>
                  <button className="cart-item-remove" onClick={() => handleRemove(item.productId)}>Remove</button>
                </div>
              ))}
              <h4>Total: R {cartTotal.toFixed(2)}</h4>
              <button className="checkout-btn" onClick={handleCheckout}>Checkout</button>
            </>
          )}
        </div>
      )}
    </div>
  );
}

export default ProductPage;
