--[[
Splash screen
]]--
states.splash = state_machine.new()
states.splash.timer = 3

function states.splash:draw()
    love.graphics.print("Splash", 0, 0)
end

function states.splash:keypressed(key, code)
    if code == "space" then state_machine.switch(states.main_menu) end
end

function states.splash:update(dt)
    self.timer = self.timer - dt
    if self.timer <= 0 then state_machine.switch(states.main_menu) end
end
