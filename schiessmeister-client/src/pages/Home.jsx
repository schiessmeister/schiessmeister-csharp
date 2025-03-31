const Home = () => {
	const competitions = ['Sedef', 'Timon', 'Luca'];

	return (
		<main>
			<h2>Wettbewerb öffnen</h2>
			<div>
				{competitions.map((comp) => (
					<button key={comp}>{comp}</button>
				))}
			</div>
			<h2>Wettbewerb erstellen</h2>
			<button type="submit">Erstellen</button>
		</main>
	);
};

export default Home;
