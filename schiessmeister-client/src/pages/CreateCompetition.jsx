const CreateCompetition = () => {
	return (
		<main>
			<h2>Wettbewerb erstellen</h2>

			<input type="name" placeholder="Name" onChange={(e) => setName(e.target.value)} />

			<input type="standort" placeholder="Standort" onChange={(e) => setStandort(e.target.value)} />

			<div>Datum</div>

			<input type="datetime-local" placeholder="Datum" onChange={(e) => setDatum(e.target.value)} />

			<button class="button" type="submit">
				Speichern
			</button>

			<button type="reset" class="button button--secondary">
				Abbrechen
			</button>
		</main>
	);
};

export default CreateCompetition;
