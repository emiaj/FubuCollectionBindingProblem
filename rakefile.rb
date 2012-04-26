require 'rubygems'
require 'albacore'
require 'rake/clean'
include FileTest

COMPILE_TARGET = (ENV['config'] || "Debug")
CLR_TOOLS_VERSION = "v4.0.30319"
WORKING_DIR = File.dirname(__FILE__)

buildsupportfiles = Dir["#{WORKING_DIR}/buildsupport/*.rb"]
raise "Run `git submodule update --init` to populate your buildsupport folder." unless buildsupportfiles.any?
buildsupportfiles.each { |ext| load ext }

tools = Dir["#{WORKING_DIR}/tools/*.rb"]
tools.each { |ext| load ext }


SRC_DIR = "src"

desc "Displays a list of tasks"
task :help do
  taskHash = Hash[*(`rake.bat -T`.split(/\n/).collect { |l| l.match(/rake (\S+)\s+\#\s(.+)/).to_a }.collect { |l| [l[1], l[2]] }).flatten]

  indent = " " * 26

  puts "rake #{indent}#Runs the 'default' task"

  taskHash.each_pair do |key, value|
    if key.nil?
      next
    end
    puts "rake #{key}#{indent.slice(0, indent.length - key.length)}##{value}"
  end
end


desc "**Default**, compiles and runs tests"
task :default => [:compile]

desc "Compiles the app"
task :compile => [:clean, :restore_if_missing] do
  MSBuildRunner.compile :compilemode => COMPILE_TARGET, :solutionfile => 'src/FubuCollectionBindingProblem.sln', :clrversion => CLR_TOOLS_VERSION
end
