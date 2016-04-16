require 'rocks' ()

function love.conf(t)
	t.identity = "Ludum Dare 35"
	t.window.title = t.identity
	t.version = "0.10.1"
	t.dependencies = { "emma ~> 1.0" }
end
