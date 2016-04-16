class = require 'middleclass'
emma = require 'emma'
state_machine = require 'gamestate'
vector = require 'vector'

states = {}

function love.load()
    -- Components
    require 'components.position'
    require 'components.rotation'
    require 'components.scale'
    require 'components.sprite'

    -- Entities
    require 'entities.player'

    -- Game states
    require 'states.splash'
    require 'states.main-menu'
    require 'states.game'

    state_machine.registerEvents()
    state_machine.switch(states.splash)
end
