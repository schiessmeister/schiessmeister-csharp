import '../styles/Participantslist.css';

const ParticipantsList = () => {
	const participants = ['Teilnehmer 1', 'Teilnehmer 2', 'Teilnehmer 3'];

	return (
		<main className="container">
			<h2>Teilnehmerliste</h2>

			<div className="participants">
				{participants.map((name, index) => (
					<div key={index} className="participant">
						<span>{name}</span>
						<div className="controls">
							<ul>
								<li>
									<button class="button">
										<img src="/arrow-up.svg" alt="Nach oben" />
									</button>
								</li>
								<li>
									<button class="button">
										<img src="/arrow-down.svg" alt="Nach unten" />
									</button>
								</li>
								<li>
									<button class="button">
										<img src="/trash-icon.svg" alt="Löschen" />
									</button>
								</li>
							</ul>
						</div>
					</div>
				))}
			</div>

			<hr />

			<div className="participant-list">
				{participants.map((name, index) => (
					<button key={index} className="button participant-button">
						{name}
					</button>
				))}
			</div>

			<hr />

			<div className="actions">
				<input type="text" placeholder="Teilnehmer" />

				<div className="buttons">
					<button className="button button--tertiary">+ Teilnehmer erstellen</button>
					<button className="button" type="submit">
						Speichern
					</button>
					<button type="reset" className="button button--secondary">
						Abbrechen
					</button>
				</div>
			</div>
		</main>
	);
};

export default ParticipantsList;
