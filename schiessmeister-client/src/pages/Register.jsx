import { useState } from "react";

const Register = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log("Registrierung mit:", email, password);
  };

  return (
    <div>
      <h2>Registrieren</h2>
      <form onSubmit={handleSubmit}>
        <input type="email" placeholder="E-Mail" value={email} onChange={(e) => setEmail(e.target.value)} />
        <input type="password" placeholder="Passwort" value={password} onChange={(e) => setPassword(e.target.value)} />
        <button type="submit">Registrieren</button>
      </form>
    </div>
  );
};

export default Register;
