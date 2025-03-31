export const API_BASE_URL = 'https://localhost:7087/api';

export const createApi = (token, handleUnauthorized) => {
	const fetchWithAuth = async (endpoint, options = {}) => {
		const headers = {
			'Content-Type': 'application/json',
			...(token && { Authorization: `Bearer ${token}` }),
			...options.headers
		};

		const response = await fetch(`${API_BASE_URL}${endpoint}`, {
			...options,
			headers
		});

		if (response.status === 401) {
			handleUnauthorized();
			throw new Error('Unauthorized');
		}

		if (!response.ok) {
			throw new Error(`HTTP error! status: ${response.status}`);
		}

		return response.json();
	};

	return {
		get: (endpoint) => fetchWithAuth(endpoint),
		post: (endpoint, data) =>
			fetchWithAuth(endpoint, {
				method: 'POST',
				body: JSON.stringify(data)
			}),
		put: (endpoint, data) =>
			fetchWithAuth(endpoint, {
				method: 'PUT',
				body: JSON.stringify(data)
			}),
		delete: (endpoint) =>
			fetchWithAuth(endpoint, {
				method: 'DELETE'
			})
	};
};
