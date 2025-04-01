import { Link } from 'react-router-dom';
import '../styles/Home.css';

const Home = () => {
	const competitions = ['Sedef', 'Timon', 'Luca'];

	return (
		<main>
			<h2>Wettbewerb öffnen</h2>

			<div className="comp-list">
				{competitions.map((comp) => (
					<button class="button" key={comp}>
						{comp}
					</button>
				))}
			</div>

			<h2>Wettbewerb erstellen</h2>

			<Link to="/createcompetition">
				<button class="button">Erstellen</button>
			</Link>
		</main>
	);
};

export default Home;
