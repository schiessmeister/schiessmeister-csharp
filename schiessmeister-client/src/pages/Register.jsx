import { useState } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { API_BASE_URL } from '../utils/api';

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
			const response = await fetch(API_BASE_URL + '/authenticate/register', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({ username, email, password })
			});

			if (!response.ok) {
				const errorData = await response.json();
				throw new Error(errorData || 'Registration failed');
			}

			// After successful registration, automatically log in
			const loginResponse = await fetch(API_BASE_URL + '/authenticate/login', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({ username, password })
			});

			if (!loginResponse.ok) {
				throw new Error('Registration successful but login failed');
			}

			const data = await loginResponse.json();
			login(data.token);
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
