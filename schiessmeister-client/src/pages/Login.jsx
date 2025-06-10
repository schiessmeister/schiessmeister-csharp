import { useState } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { loginRequest } from '../api/authService';

const Login = () => {
	const [username, setUsername] = useState('');
	const [password, setPassword] = useState('');
	const [error, setError] = useState('');
	const { login } = useAuth();

	const handleSubmit = async (e) => {
		e.preventDefault();
		setError('');

               try {
                        const data = await loginRequest(username, password);
                        login(data.token, data.id);
                } catch (error) {
                        setError('Invalid username or password');
                        console.error('Login error:', error);
                }
	};

	return (
		<main>
			<h2>Melde dich an</h2>

			<form onSubmit={handleSubmit}>
				<div>
					<label htmlFor="username">Username</label>
					<input id="username" name="username" type="text" required placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} />
				</div>

				<div>
					<label htmlFor="password">Passwort</label>
					<input id="password" name="password" type="password" required placeholder="Passwort" value={password} onChange={(e) => setPassword(e.target.value)} />
				</div>

				{error && <div>{error}</div>}

				<button className="button" type="submit">
					Anmelden
				</button>
			</form>

			<Link to="/register">Sie haben keinen Account? Registrieren</Link>
		</main>
	);
};

export default Login;
