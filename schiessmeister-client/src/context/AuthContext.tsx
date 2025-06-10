import { createContext, useContext, useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'

export interface AuthContextType {
  token: string | null
  userId: string | null
  login: (token: string, userId: string) => void
  logout: () => void
  handleUnauthorized: () => void
}

const AuthContext = createContext<AuthContextType | undefined>(undefined)

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
        const [token, setToken] = useState<string | null>(localStorage.getItem('token'));
        const [userId, setUserId] = useState<string | null>(localStorage.getItem('userId'));
        const navigate = useNavigate();

	useEffect(() => {
		if (token) {
			localStorage.setItem('token', token);
		} else {
			localStorage.removeItem('token');
		}
	}, [token]);

	useEffect(() => {
		if (userId) {
			localStorage.setItem('userId', userId);
		} else {
			localStorage.removeItem('userId');
		}
	}, [userId]);

        const login = (newToken: string, newUserId: string) => {
                setToken(newToken);
                setUserId(newUserId);
                navigate('/home');
        };

        const logout = () => {
                setToken(null);
                setUserId(null);
                navigate('/login');
        };

        const handleUnauthorized = () => {
                logout();
        };

	return <AuthContext.Provider value={{ token, userId, login, logout, handleUnauthorized }}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
	const context = useContext(AuthContext);
	if (!context) {
		throw new Error('useAuth must be used within an AuthProvider');
	}
	return context;
};
