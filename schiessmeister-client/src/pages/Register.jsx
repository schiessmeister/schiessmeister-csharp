import { useState } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { registerRequest, loginRequest } from '../api/authService';

const Register = () => {
	const [username, setUsername] = useState('');
	const [email, setEmail] = useState('');
	const [password, setPassword] = useState('');
	const [error, setError] = useState('');
	const { login } = useAuth();

	const handleSubmit = async (e) => {
		e.preventDefault();
		setError('');

               try {
                        await registerRequest(username, email, password);
                        const data = await loginRequest(username, password);
                        login(data.token, data.id);
                } catch (error) {
                        setError(error.message || 'Registration failed');
                        console.error('Registration error:', error);
                }
	};

	return (
		<main>
			<h2>Account erstellen</h2>

			<form onSubmit={handleSubmit}>
				<div>
					<label htmlFor="username">Username</label>
					<input id="username" name="username" type="text" required placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} />
				</div>

				<div>
					<label htmlFor="email">Email</label>
					<input id="email" name="email" type="email" required placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
				</div>

				<div>
					<label htmlFor="password">Passwort</label>
					<input id="password" name="password" type="password" required placeholder="Passwort" value={password} onChange={(e) => setPassword(e.target.value)} />
				</div>

				{error && <div>{error}</div>}

				<button className="button" type="submit">
					Registrieren
				</button>
			</form>

			<Link to="/login">Sie haben schon einen Account? Anmelden</Link>
		</main>
	);
};

export default Register;
