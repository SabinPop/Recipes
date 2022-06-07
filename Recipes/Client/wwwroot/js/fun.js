const darkThemeMediaQuery = window.matchMedia("(prefers-color-scheme: dark)");

window.darkModeChange = (dotNetHelper) => {
	return darkThemeMediaQuery.matches;
};

function IsDarkTheme() {
	return darkThemeMediaQuery.matches;
}

function ScrollToTop() {
	window.scrollTo({ top: 0, behavior: 'smooth' });
}